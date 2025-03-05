using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class ProgressStream : Stream
{
    private readonly Stream innerStream;
    private readonly Action<long, long> progressCallback;
    private long totalBytesTransferred = 0;
    private readonly long totalLength;

    public ProgressStream(Stream innerStream, long totalLength, Action<long, long> progressCallback)
    {
        this.innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
        this.totalLength = totalLength;
        this.progressCallback = progressCallback;
    }

    public override bool CanRead => innerStream.CanRead;
    public override bool CanSeek => innerStream.CanSeek;
    public override bool CanWrite => innerStream.CanWrite;
    public override long Length => innerStream.Length;
    public override long Position { get => innerStream.Position; set => innerStream.Position = value; }
    public override void Flush() => innerStream.Flush();
    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = innerStream.Read(buffer, offset, count);
        totalBytesTransferred += bytesRead;
        progressCallback?.Invoke(totalBytesTransferred, totalLength);
        return bytesRead;
    }
    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int bytesRead = await innerStream.ReadAsync(buffer, offset, count, cancellationToken);
        totalBytesTransferred += bytesRead;
        progressCallback?.Invoke(totalBytesTransferred, totalLength);
        return bytesRead;
    }
    public override void Write(byte[] buffer, int offset, int count)
    {
        innerStream.Write(buffer, offset, count);
        totalBytesTransferred += count;
        progressCallback?.Invoke(totalBytesTransferred, totalLength);
    }
    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await innerStream.WriteAsync(buffer, offset, count, cancellationToken);
        totalBytesTransferred += count;
        // Only invoke if we haven't already exceeded totalLength.
        if (totalBytesTransferred <= totalLength)
            progressCallback?.Invoke(totalBytesTransferred, totalLength);
    }

    public override long Seek(long offset, SeekOrigin origin) => innerStream.Seek(offset, origin);
    public override void SetLength(long value) => innerStream.SetLength(value);
}
