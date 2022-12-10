namespace lego_indexor_api.API.Websockets;

public static class MessageType
{
    public const int NewConnection = 0;
    public const int TakePicture = 1;
    public const int PictureTaken = 2;
    public const int PictureDownloaded = 3;
}