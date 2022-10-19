namespace StuartAitken.Blazor.Client.Constants
{
    public static class Routes
    {
        #region Public Classes

        public static class Api
        {
            #region Public Fields

            public const string Auth = BaseApi + "auth/";
            public const string BaseApi = "api/";

            /// <summary>
            /// Use for getting standard image file, for html image elements
            /// </summary>
            public const string ProjectImage_AsObject = ProjectImages + "image-object/";

            public const string ProjectImages = BaseApi + "project-images/";
            public const string ProjectImages_ForProject = ProjectImages + "images-for-project/";

            public const string ProjectImages_IdsForProject =
                ProjectImages + "image-ids-for-project/";

            public const string ProjectImages_SetMainImage = ProjectImages + "set-main-image/";
            public const string Projects = BaseApi + "projects/";

            public const string Projects_All = Projects + "all";
            public const string Projects_SelectList = Projects + "select-list";
            public const string Projects_Techs = Projects + "techs";
            public const string Projects_Types = Projects + "types";

            #endregion Public Fields
        }

        public static class Pages
        {
            #region Public Fields

            public const string CV = "/cv";
            public const string EditProject = "/projects/edit";
            public const string Index = "/";
            public const string Projects = "/projects";
            public const string Swagger = "/swagger/index.html";

            #endregion Public Fields
        }

        #endregion Public Classes
    }
}
