namespace TWAddIn.Services
{

    public class FileService
    {
        private HttpUtility httpUtility;

        public FileService()
        {
            this.httpUtility = new HttpUtility();
        }

        public void UploadFile(string fileName)
        {
            this.httpUtility.PostFile(fileName, UserService.User.Id, RequestType.FilePost);
        }
    }
}