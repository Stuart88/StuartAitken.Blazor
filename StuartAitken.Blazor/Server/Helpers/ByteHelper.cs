namespace StuartAitken.Blazor.Server.Helpers
{
    public static class ByteHelper
    {
        #region Public Methods

        public static byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        #endregion Public Methods
    }
}
