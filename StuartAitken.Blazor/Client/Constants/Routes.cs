namespace StuartAitken.Blazor.Client.Constants
{
    public static class Constants
    {
        public const string SiteName = "stuart-aitken.co.uk";
    }

    public static class Routes
    {
        public static class Pages
        {
            public const string Index = "/";
            public const string Projects = "/projects";
            public const string Swagger = "/swagger/index.html";
        }

        public static class Api
        {
            public const string BaseApi = "api/";

            public const string Projects = BaseApi + "projects/";

            public const string Projects_All = Projects + "all";
            public const string Projects_SelectList = Projects + "select-list";

            public const string ProjectImages = BaseApi + "project-images/";
            public const string ProjectImages_IdsForProject =
                ProjectImages + "image-ids-for-project/";
            public const string ProjectImages_ForProject = ProjectImages + "images-for-project/";

            /// <summary>
            /// Use for getting standard image file, for html image elements
            /// </summary>
            public const string ProjectImage_AsObject = ProjectImages + "image-object/";
        }
    }
}
