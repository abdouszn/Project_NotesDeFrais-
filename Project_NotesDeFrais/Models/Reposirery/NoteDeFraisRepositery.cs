using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    public class NoteDeFraisRepositery : DbContext
    {
        NotesDeFraisEntities e;
        public NoteDeFraisRepositery()
        {
            e = new NotesDeFraisEntities();
        }

        public IQueryable<ProjectsModel> all()
        {
            var project = (IQueryable < ProjectsModel >) e.Projects.ToList();
            return project;
        }
    }
}


    
