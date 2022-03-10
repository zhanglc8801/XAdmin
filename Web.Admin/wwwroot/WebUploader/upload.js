(function ($) {
    var INTEROP_PATH = {
        //swf所在路径
        swf: '~/WebUploader/Uploader.swf',
        //处理文件上传的地址
        server: '/FileHandle/Upload',
        //获取已上传文件的块数量
        GetMaxChunk: '/FileHandle/GetMaxChunk',
        //进行文件合并的地址
        MergeFiles: "/FileHandle/MergeFiles"
    };

    // 当domReady的时候开始初始化
    $(function () {
        var $wrap = $('#uploader'),

        // 图片容器
            $queue = $('<ul class="filelist"></ul>')
                .appendTo($wrap.find('.queueList')),

        // 状态栏，包括进度和控制按钮
            $statusBar = $wrap.find('.statusBar'),

        // 文件总体选择信息。
            $info = $statusBar.find('.info'),
        // 上传按钮
            $upload = $wrap.find('.uploadBtn'),

        // 没选择文件之前的内容。
            $placeHolder = $wrap.find('.placeholder'),
            $progress = $statusBar.find('.progress').hide(),

        // 添加的文件数量
            fileCount = 0,
        // 添加的文件总大小
            fileSize = 0,

        // 优化retina, 在retina下这个值是2
            ratio = window.devicePixelRatio || 1,

        // 缩略图大小
            thumbnailWidth = 110 * ratio,
            thumbnailHeight = 110 * ratio,

        // 可能有pedding, ready, uploading, confirm, done.
            state = 'pedding',

        // 所有文件的进度信息，key为file id
            percentages = {},
        // 判断浏览器是否支持图片的base64
            isSupportBase64 = (function () {
                var data = new Image();
                var support = true;
                data.onload = data.onerror = function () {
                    if (this.width != 1 || this.height != 1) {
                        support = false;
                    }
                };
                data.src = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
                return support;
            })(),

        // 检测是否已经安装flash，检测flash的版本
            flashVersion = (function () {
                var version;
                try {
                    version = navigator.plugins['Shockwave Flash'];
                    version = version.description;
                } catch (ex) {
                    try {
                        version = new ActiveXObject('ShockwaveFlash.ShockwaveFlash')
                                .GetVariable('$version');
                    } catch (ex2) {
                        version = '0.0';
                    }
                }
                version = version.match(/\d+/g);
                return parseFloat(version[0] + '.' + version[1], 10);
            })(),

            supportTransition = (function () {
                var s = document.createElement('p').style,
                    r = 'transition' in s ||
                            'WebkitTransition' in s ||
                            'MozTransition' in s ||
                            'msTransition' in s ||
                            'OTransition' in s;
                s = null;
                return r;
            })(),

        // WebUploader实例
            uploader,
            GUID = WebUploader.Base.guid(); //当前页面是生成的GUID作为标示

        if (!WebUploader.Uploader.support('flash') && WebUploader.browser.ie) {
            // flash 安装了但是版本过低。
            if (flashVersion) {
                (function (container) {
                    window['expressinstallcallback'] = function (state) {
                        switch (state) {
                            case 'Download.Cancelled':
                                alert('您取消了更新！');
                                break;

                            case 'Download.Failed':
                                alert('安装失败');
                                break;

                            default:
                                alert('安装已成功，请刷新！');
                                break;
                        }
                        delete window['expressinstallcallback'];
                    };

                    var swf = 'Scripts/expressInstall.swf';
                    // insert flash object
                    var html = '<object type="application/' +
                            'x-shockwave-flash" data="' + swf + '" ';

                    if (WebUploader.browser.ie) {
                        html += 'classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" ';
                    }

                    html += 'width="100%" height="100%" style="outline:0">' +
                        '<param name="movie" value="' + swf + '" />' +
                        '<param name="wmode" value="transparent" />' +
                        '<param name="allowscriptaccess" value="always" />' +
                    '</object>';
                    container.html(html);
                })($wrap);

                // 压根就没有安装。
            } else {
                $wrap.html('<a href="http://www.adobe.com/go/getflashplayer" target="_blank" border="0"><img alt="get flash player" src="http://www.adobe.com/macromedia/style_guide/images/160x41_Get_Flash_Player.jpg" /></a>');
            }

            return;
        } else if (!WebUploader.Uploader.support()) {
            alert('Web Uploader 不支持您的浏览器！');
            return;
        }

        // 实例化
        uploader = WebUploader.create({
            pick: {
                id: '#filePicker',
                label: '选择文件'
            },
            formData: {
                uid: 123
            },
            paste: '#uploader',
            swf: INTEROP_PATH.swf,
            chunked: true, //分片处理大文件
            chunkSize: 2 * 1024 * 1024,
            //duplicate: true,//允许上传同一文件 https://my.oschina.net/zchuanzhao/blog/757311
            server: INTEROP_PATH.server,
            disableGlobalDnd: true,
            threads: 2, //上传并发数
            //由于Http的无状态特征，在往服务器发送数据过程传递一个进入当前页面是生成的GUID作为标示
            formData: {},
            fileNumLimit: 1,
            compress: false, //图片在上传前不进行压缩
            fileSizeLimit: 1024 * 1024 * 1024 * 1024,    // 1024 G
            fileSingleSizeLimit: 1024 * 1024 * 1024 * 1024    // 1024 G
        });

        var f;
        uploader.on('fileQueued', function (file) {
            f = file;
            if (window.QueuedSuccessCallback) {
                window.QueuedSuccessCallback();
            }
            uploader.md5File(file)
                // 及时显示进度
                .progress(function (percentage) {
                    var fileObj = $('#' + file.id);
                    var spanObj = fileObj.find('.progress_check>span').text(parseInt(percentage * 100));
                    //if (percentage == 1) {
                        //fileObj.find('.progress_check').hide();
                        //fileObj.find('.progress_check').attr('data-checkedcomplete', true).text('验证完成，等待上传').css('color', '#aaa');
                    //}
                    //console.log('Percentage:', percentage, file.getStatus(), file);
                })
                // 完成
                .then(function (val) {
                    console.info('from data:', uploader.options.formData);
                    console.info('md5 result:', val, file);
                    var temp_obj = {};
                    var temp_key = file.id + "md5";
                    temp_obj[temp_key] = val;
                    $.extend(uploader.options.formData, temp_obj);
                    var fileObj = $('#' + file.id);
                    $.ajax({
                        url: INTEROP_PATH.GetMaxChunk,
                        async: true,
                        data: { md5: val, ext: file.ext },
                        success: function (response) {     
                            if (response.code != 1) {
                                alert(response.msg);
                                uploader.removeFile(file);
                                return;
                            }
                            else {
                                //$.extend(uploader.options.formData, { chunk: res.chunk });
                                Global.FileQueueds.push({ id: file.id, md5: val, size: file.size, ext: file.ext, chunk: response.chunk, savePath: response.savePath });
                                //console.info('fileCheckMaxChunk', file, response);
                                fileObj.find('.progress_check').attr('data-checkedcomplete', true).text('验证完成，等待上传').css('color', '#aaa');
                                $upload.css("display", "");
                                $("#filePicker").css("display", "none");
                                //文件验证完成后自动触发上传
                                //uploader.upload();
                            }   
                        }
                    });
                });

        });


        uploader.on('dialogOpen', function () {
        });

        // uploader.on('filesQueued', function() {
        //     uploader.sort(function( a, b ) {
        //         if ( a.name < b.name )
        //           return -1;
        //         if ( a.name > b.name )
        //           return 1;
        //         return 0;
        //     });
        // });

        // 添加“添加文件”的按钮，
        //uploader.addButton({
        //    id: '#filePicker2',
        //    label: '继续添加'
        //});

        uploader.on('ready', function () {
            window.uploader = uploader;
        });

        // 当有文件添加进来时执行，负责view的创建
        function addFile(file) {
            var $li = $('<li id="' + file.id + '">' +
                    '<p class="title">' + file.name + '</p>' +
                    '<p class="progress"><span></span></p>' +
                    '<p class="progress_check" data-checkedcomplete="false">正在验证文件：<span>0</span>%</p>' +
                    '</li>'),

                $btns = $('<div class="file-panel"><span class="cancel">删除</span></div>').appendTo($li),
                $prgress = $li.find('p.progress span'),
                $info = $('<p class="error"></p>'),

                showError = function (code) {
                    switch (code) {
                        case 'exceed_size':
                            text = '文件大小超出';
                            break;
                        case 'interrupt':
                            text = '上传暂停';
                            break;
                        default:
                            text = '上传失败，请重试';
                            break;
                    }
                    $info.text(text).appendTo($li);
                };

            if (file.getStatus() === 'invalid') {
                showError(file.statusText);
            } else {
                // @todo lazyload
                percentages[file.id] = [file.size, 0];
                file.rotation = 0;
            }

            file.on('statuschange', function (cur, prev) {
                if (prev === 'progress') {
                    //$prgress.hide().width(0);//暂停上传时 不隐藏进度
                } else if (prev === 'queued') {
                    $li.off('mouseenter mouseleave');
                    $btns.remove();
                }
                // 成功
                if (cur === 'error' || cur === 'invalid') {
                    console.log(file.statusText);
                    showError(file.statusText);
                    percentages[file.id][1] = 1;
                } else if (cur === 'interrupt') {
                    //showError('interrupt');//暂停上传时 不显示暂停上传
                } else if (cur === 'queued') {
                    $info.remove();
                    $prgress.css('display', 'block');
                    percentages[file.id][1] = 0;
                } else if (cur === 'progress') {
                    $info.remove();
                    $prgress.css('display', 'block');
                } else if (cur === 'complete') {
                    $prgress.hide().width(0);
                    var fileObj = $('#' + file.id);
                    fileObj.find('.progress_check').show();
                    fileObj.find('.progress_check').attr('data-checkedcomplete', true).text('上传完成!').css('color', 'green');

                    //$prgress.show().text("上传完成");
                    $li.append('<span class="success"></span>');
                    $("#uploader .delete").css("display","block");
                }
                $li.removeClass('state-' + prev).addClass('state-' + cur);
            });

            $li.on('mouseenter', function () {
                $btns.stop().animate({ height: 30 });
            });

            $li.on('mouseleave', function () {
                $btns.stop().animate({ height: 0 });
            });

            $btns.on('click', 'span', function () {
                var index = $(this).index(),
                    deg;

                switch (index) {
                    case 0:
                        uploader.removeFile(file);
                        return;

                    case 1:
                        file.rotation += 90;
                        break;

                    case 2:
                        file.rotation -= 90;
                        break;
                }

                if (supportTransition) {
                    deg = 'rotate(' + file.rotation + 'deg)';
                    $wrap.css({
                        '-webkit-transform': deg,
                        '-mos-transform': deg,
                        '-o-transform': deg,
                        'transform': deg
                    });
                } else {
                    $wrap.css('filter', 'progid:DXImageTransform.Microsoft.BasicImage(rotation=' + (~ ~((file.rotation / 90) % 4 + 4) % 4) + ')');
                }
            });

            $li.appendTo($queue);
        }

        // 负责view的销毁
        function removeFile(file) {
            var $li = $('#' + file.id);
            delete percentages[file.id];
            updateTotalProgress();
            $li.off().find('.file-panel').off().end().remove();
        }

        function updateTotalProgress() {
            var loaded = 0,
                total = 0,
                spans = $progress.children(),
                percent;

            $.each(percentages, function (k, v) {
                total += v[0];
                loaded += v[0] * v[1];
            });
            percent = total ? loaded / total : 0;
            spans.eq(0).text(Math.round(percent * 100) + '%');
            spans.eq(1).css('width', Math.round(percent * 100) + '%');
            updateStatus();
        }

        function updateStatus() {
            var text = '', stats;
            if (state === 'ready') {
                //text = '选中' + fileCount + '个文件，共' + WebUploader.formatSize(fileSize) + '。';
                text = "";
            } else if (state === 'confirm') {
                stats = uploader.getStats();
            } else {
                stats = uploader.getStats();
            }
            $info.html(text);
        }

        function setState(val) {
            var file, stats;
            if (val === state) {
                return;
            }
            $upload.removeClass('state-' + state);
            $upload.addClass('state-' + val);
            state = val;
            switch (state) {
                case 'pedding':
                    $placeHolder.removeClass('element-invisible');
                    $queue.hide();
                    $statusBar.addClass('element-invisible');
                    uploader.refresh();
                    break;

                case 'ready':
                    $placeHolder.addClass('element-invisible');
                    $('#filePicker2').removeClass('element-invisible');
                    $queue.show();
                    $statusBar.removeClass('element-invisible');
                    uploader.refresh();
                    break;

                case 'uploading':
                    $('#filePicker2').addClass('element-invisible');
                    //$progress.show();
                    $upload.text('暂停上传');
                    break;

                case 'paused':
                    $.each(uploader.getFiles(), function (idx, row) {
                        if (row.getStatus() == "progress") {
                            row.setStatus('interrupt');
                        }
                    });
                    //uploader.getFiles()[0].setStatus('interrupt');
                    //$progress.show();
                    $upload.text('继续上传');
                    break;

                case 'confirm':
                    //$progress.hide();
                    $('#filePicker2').removeClass('element-invisible');
                    $upload.text('开始上传');
                    stats = uploader.getStats();
                    if (stats.successNum && !stats.uploadFailNum) {
                        setState('finish');
                        return;
                    }
                    break;
                case 'finish':
                    stats = uploader.getStats();
                    if (stats.successNum) {
                        console.info('finish', uploader, stats);
                        //alert('上传完成');
                        //uploader.removeFile(uploader.getFile(f.id));
                        //uploader.onFileDequeued = function (file) {
                        //    $upload.css("display", "none");
                        //    $("#filePicker").css("display", "");
                        //    $("#" + f.id).css("display","none");
                        //};

                        if (window.UploadSuccessCallback) {
                            window.UploadSuccessCallback();
                        }
                    } else {
                        // 没有成功的图片，重设
                        state = 'done';
                        location.reload();
                    }
                    break;
            }
            updateStatus();
        }

        uploader.onUploadProgress = function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');

            $percent.css('width', percentage * 100 + '%');
            $percent.text(Math.round(percentage * 100) + '%');
            percentages[file.id][1] = percentage;
            updateTotalProgress();
        };

        uploader.onFileQueued = function (file) {
            fileCount++;
            fileSize += file.size;

            if (fileCount === 1) {
                $placeHolder.addClass('element-invisible');
                //$statusBar.show();
            }

            addFile(file);
            setState('ready');
            updateTotalProgress();
        };

        uploader.onFileDequeued = function (file) {
            fileCount--;
            fileSize -= file.size;
            if (!fileCount) {
                setState('pedding');
            }
            removeFile(file);
            updateTotalProgress();
            $upload.css("display", "none");
            $("#filePicker").css("display", "");
        };

        //删除文件
        $("#uploader .delete").click(function () {
            //alert(fpath);
            //alert(fmd5);
            //alert(f.name);
            $.each(uploader.getFiles(), function (idx, row) {
                ////物理文件删除示例
                //$.post(applicationPath + '/ajax/WebUploader/Delete', { 'filePathName': row.fpath }, function (returndata) {
                //});
                uploader.removeFile(uploader.getFile(row.id));
            });
            $("#uploader .delete").css("display", "none");
        });

        //all算是一个总监听器
        uploader.on('all', function (type, arg1, arg2) {
            //console.log("all监听：", type, arg1, arg2);
            var stats;
            switch (type) {
                case 'uploadFinished':
                    setState('confirm');
                    break;
                case 'startUpload':
                    setState('uploading');
                    break;
                case 'stopUpload':
                    setState('paused');
                    break;
            }
        });

        // 文件上传成功,合并文件。
        uploader.on('uploadSuccess', function (file, response) {
            console.info('uploadSuccess', file, response);

            if (response && response.code >= 0) {
                //var dataObj = JSON.parse(response.data);
                var md5 = Global.GetFileQueued(file.id).md5;
                var savePath = response.savePath;
                var virtualPath = response.virtualPath;
                //alert(dataObj.chunked);
                //alert(dataObj.savePath);
                if (response.chunked) {
                    $.post(INTEROP_PATH.MergeFiles, { md5: md5, ext: response.ext },
                    function (data) {
                        //data = $.parseJSON(data);
                        //var ddd = JSON.parse(data.data);
                        //alert(decodeURIComponent(ddd.savePath));

                        if (response.hasError) {
                            alert('文件合并失败！');
                        } else {
                            console.info('上传文件完成并合并成功，触发回调事件');
                            if (window.UploadSuccessCallback) {
                                window.UploadSuccessCallback(file, md5, savePath, virtualPath);
                            }
                        }
                    });
                }
                else {
                    console.info('上传文件完成，不需要合并，触发回调事件');
                    if (window.UploadSuccessCallback) {
                        window.UploadSuccessCallback(file, md5, savePath, virtualPath);
                    }
                }
            }
        });

        uploader.onError = function (code) {
            alert('Eroor: ' + code);
        };

        $upload.on('click', function () {
            if ($(this).hasClass('disabled')) {
                return false;
            }
            if ($queue.find('.progress_check[data-checkedcomplete=false]').length > 0) {
                alert('请等待文件验证完成');
                return false;
            }
            else {
                $queue.find('.progress_check').hide();
            }

            if (state === 'ready') {
                uploader.upload();
            } else if (state === 'paused') {
                uploader.upload();
            } else if (state === 'uploading') {
                uploader.stop();
            }
        });

        $info.on('click', '.retry', function () {
            uploader.retry();
        });

        $info.on('click', '.ignore', function () {
            alert('todo');
        });

        $upload.addClass('state-' + state);
        updateTotalProgress();
    });

})(jQuery);
