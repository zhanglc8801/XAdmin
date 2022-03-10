using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace XAdmin.Common.Utils
{
    public class FFmpegUtil
    {
        /// <summary>
        /// 获取视频长度 秒数
        /// </summary>
        public static async Task<VideoInfo> GetVideoInfo(string ffmpegPath,string videoPath)
        {
            VideoInfo vi = new VideoInfo();
            FFmpeg.SetExecutablesPath(ffmpegPath);
            var mediaInfo = await FFmpeg.GetMediaInfo(videoPath);
            vi.Length= mediaInfo.Duration.TotalSeconds;
            vi.Height = mediaInfo.VideoStreams.First().Height;
            vi.Width = mediaInfo.VideoStreams.First().Width;
            return vi;
        }

        /// <summary>
        /// 截取视频的第几帧
        /// </summary>
        /// <param name="ffmpegPath">ffmpeg路径</param>
        /// <param name="videoPath">视频路径</param>
        /// <param name="thumbnailPath">缩略图存储路径</param>
        /// <param name="frameIndex">要截取第几帧</param>
        public async static void GetVideoThumbnail(string ffmpegPath, string videoPath,string thumbnailPath,int frameIndex)
        {
            FFmpeg.SetExecutablesPath(ffmpegPath);
            Func<string, string> outputFileNameBuilder = (number) => { return thumbnailPath; };
            IMediaInfo info = await FFmpeg.GetMediaInfo(videoPath).ConfigureAwait(false);
            IVideoStream videoStream = info.VideoStreams.First()?.SetCodec(VideoCodec.png);

            FileInfo fi = new FileInfo(thumbnailPath);
            if (fi.Exists)
            {
                fi.Delete();
            }
            IConversionResult conversionResult = await FFmpeg.Conversions.New()
                .AddStream(videoStream)
                .ExtractNthFrame(frameIndex, outputFileNameBuilder)
                .Start();
        }


    }

    public class VideoInfo
    {
        public double Length { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
