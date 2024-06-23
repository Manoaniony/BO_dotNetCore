using System;
using System.ComponentModel.DataAnnotations;

namespace ArchiveNetCore.Models
{
	public class Entrepots
	{
        public int Id { get; set; }
        public string libelle { get; set; }
        public int nbre_rayon { get; set; }
        public int nbre_niveau{ get; set; }
        public int nbre_range { get; set; }
        public string desc { get; set; }

    }
}

