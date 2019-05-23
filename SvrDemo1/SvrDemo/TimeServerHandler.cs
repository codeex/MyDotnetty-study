/*****************************************************************
 * Copyright (C) 2019 上海科箭软件科技有限公司版权所有。 
 *
 * 文件名称    ：TimeServer  
 * 创 建 者    ：Mark
 * 创建日期    ：2019/5/22 星期三 18:20:28 
 *
 * 功能描述：  
******************************************************************/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace SvrDemo
{
    internal class TimeServerHandler : ChannelHandlerAdapter
    {
        public TimeServerHandler()
        {
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer buf = (IByteBuffer)message;
            byte[] req = new byte[buf.ReadableBytes];
            buf.ReadBytes(req);
            //ReadOnlySpan<byte> bytesBuffer = req;
            //ReadOnlySpan<sbyte> sbytesBuffer = MemoryMarshal.Cast<byte, sbyte>(bytesBuffer);
            //sbyte[] signed = sbytesBuffer.ToArray();
            string body = Encoding.UTF8.GetString(req);
            Trace.WriteLine($"recv order: {body}");
            string currentTime = string.Compare(body, "query time", true)==0 ? DateTime.Now.ToLongTimeString() : "bad order";
            IByteBuffer resp = Unpooled.CopiedBuffer(currentTime, Encoding.UTF8);
            context.WriteAsync(resp);
            //string body = new string(signed, 0, req.Length, Encoding.UTF8);
            //base.ChannelRead(context, message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);
            context.CloseAsync();
        }

    }
}