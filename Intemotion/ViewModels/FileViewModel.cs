
using Intemotion.Entities;

namespace Intemotion.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel(FileModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Path = model.Path;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
