﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;
using FMS.FrameWork;
using FMS.Infrastructure;

namespace FMS.Core.Service
{
   public class ProjectSkillService:IProjectSkillService
    {
       FMSDbContext _context;

       public ProjectSkillService(FMSDbContext context)
        {
            _context = context;
        }
       public Result<ProjectSkills> Save(ProjectSkills userinfo)
       {
           var result = new Result<ProjectSkills>();
           try
           {
               var objtosave = _context.projectSkillses.FirstOrDefault(u => u.PostId == userinfo.PostId);
               if (objtosave == null)
               {
                   objtosave = new ProjectSkills();
                   _context.projectSkillses.Add(objtosave);
               }
               objtosave.SkillId = userinfo.SkillId;


               if (!IsValid(objtosave, result))
               {
                   return result;
               }
               _context.SaveChanges();
           }
           catch (Exception ex)
           {
               result.HasError = true;
               result.Message = ex.Message;
           }
           return result;
       }

       public bool IsValid(ProjectSkills obj, Result<ProjectSkills> result)
       {
           if (!ValidationHelper.IsStringValid(obj.SkillId.ToString()))
           {
               result.HasError = true;
               result.Message = "Invalid SkillID";
               return false;
           }



           return true;
       }

       public Result<List<ProjectSkills>> GetAll(string key = "")
       {
           var result = new Result<List<ProjectSkills>>() { Data = new List<ProjectSkills>() };

           try
           {
               IQueryable<ProjectSkills> query = _context.projectSkillses;

               if (ValidationHelper.IsIntValid(key))
               {
                   query = query.Where(q => q.PostId == Int32.Parse(key));
               }

               if (ValidationHelper.IsStringValid(key))
               {
                   query = query.Where(q => q.SkillId.Equals(Int32.Parse(key)));

               }




               result.Data = query.ToList();
           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       public Result<ProjectSkills> GetByID(int id)
       {
           var result = new Result<ProjectSkills>();

           try
           {
               var obj = _context.projectSkillses.FirstOrDefault(c => c.SkillId == id);
               if (obj == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostID";
                   return result;


               }
               result.Data = obj;
           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }

       public Result<bool> Delete(int id)
       {
           var result = new Result<bool>();

           try
           {
               var objtodelete = _context.projectSkillses.FirstOrDefault(c => c.PostId == id);
               if (objtodelete == null)
               {
                   result.HasError = true;
                   result.Message = "Invalid PostID";
                   return result;


               }

               _context.projectSkillses.Remove(objtodelete);
               _context.SaveChanges();

           }
           catch (Exception e)
           {
               result.HasError = true;
               result.Message = e.Message;


           }
           return result;
       }
    }
}
