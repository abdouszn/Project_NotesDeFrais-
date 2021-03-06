﻿using System;
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

        public IQueryable<AspNetRoles> allRoles()
        {
            using (new NotesDeFraisEntities())
            {
                var rolesList = e.AspNetRoles.OrderBy(r=>r.Name);
                return rolesList;
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

        public void RoleToUser(String idRole, String idUser) {
         
        }
    }
}