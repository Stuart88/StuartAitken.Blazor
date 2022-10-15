using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Client.Helpers
{
    public static class StringHelpers
    {
        public static string TechIconSrc(string name)
        {
            switch (name)
            {
                case "C#":
                    name = "C-Sharp";
                    break;
                case "C++":
                    name = "C-PlusPlus";
                    break;
            }
            return "images/techIcons/" + name.Replace(' ', '-') + ".svg";
        }

        public static string TypeIconSrc(string name)
        {
            return "images/typeIcons/" + name.Replace(' ', '-') + ".svg";
        }

        public static string ImageAssetSrc(string name, string extension)
        {
            extension = extension.Replace(".", "");

            return $"images/assets/{name}.{extension}";
        }

        public static string MakePresentableDuration(Project p)
        {
            double d = p.ProjectDurationDays;

            if (d <= 0)
                return "Indeterminate";

            // Duration is between 1 week and 2 months. Give result in weeks.
            if (d >= 7 && d < 62)
                return $"{d / 7d:F0} Weeks";

            // Duration is between 2 months and 1 year. Give result in months.
            double monthAvgDays = 30.437;
            if (d >= 62 && d < 365)
                return $"{d / monthAvgDays:F0} Months";

            // Duration is more than 1 year. Give result in years.
            if (d >= 365)
                return $"{d / 356:F0} Years";

            // Duration is less than one week Give result in days.
            return $"{d} Days";
        }
    }
}
