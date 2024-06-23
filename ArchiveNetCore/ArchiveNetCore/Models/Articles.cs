using System;
namespace ArchiveNetCore.Models
{
	public class Articles
	{
        public int Id { get; set; }
        public string libelle { get; set; }
        public int? niv { get; set; }
        public int? rayon { get; set; }
        public int? range { get; set; }
        public int? idEntrepot { get; set; }
        public string? desc { get; set; }
    }
}

