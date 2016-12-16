using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class RolesRepositery
    {
        NotesDeFraisEntities e;
        public RolesRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void addRoles(AspNetRoles role)
        {
            using (new NotesDeFraisEntities())
            {
                e.AspNetRoles.Add(role);
                e.SaveChanges();
            }
        }
        public AspNetRoles getRole(String  name)
        {
            using (new NotesDeFraisEntities())
            {
                var role = (from s in e.AspNetRoles where s.Name == name select s).FirstOrDefault();
                return role;
            }
        }
    }
}