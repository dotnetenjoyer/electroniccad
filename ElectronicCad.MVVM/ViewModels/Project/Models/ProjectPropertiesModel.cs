using ElectronicCad.MVVM.Utils;

namespace ElectronicCad.MVVM.ViewModels.Projects.Models
{
    public class ProjectPropertiesModel : EditableModel
    {
        /// <summary>
        /// Project id.
        /// </summary>
        public Guid Id 
        { 
            get => id; 
            set => SetProperty(ref id, value); 
        }

        private Guid id;
        
        /// <summary>
        /// Project name.
        /// </summary>
        public string Name 
        { 
            get => name; 
            set => SetProperty(ref name, value);
        }

        private string name;

        /// <summary>
        /// Project file name.
        /// </summary>
        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }

        private string fileName;

        /// <summary>
        /// Project description.
        /// </summary>
        public string Description 
        {
            get => description; 
            set => SetProperty(ref description, value); 
        }

        private string description;

        /// <summary>
        /// Customer name.
        /// </summary>
        public string Customer
        {
            get => customer;
            set => SetProperty(ref customer, value);
        }

        private string customer;

        /// <summary>
        /// Customer contact
        /// </summary>
        public string CustomerContact
        {
            get => customerContact;
            set => SetProperty(ref customerContact, value);
        }

        private string customerContact;
    }
}
