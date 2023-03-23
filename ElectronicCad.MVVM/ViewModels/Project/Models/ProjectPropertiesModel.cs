using ElectronicCad.MVVM.Utils;

namespace ElectronicCad.MVVM.ViewModels.Project.Models
{
    public class ProjectPropertiesModel : EditableModel
    {
        /// <summary>
        /// Project id.
        /// </summary>
        public Guid Id { get; set; }
     
        /// <summary>
        /// Project name.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Project folder name.
        /// </summary>
        public string ProjectFolderName { get; set; }
    }
}
