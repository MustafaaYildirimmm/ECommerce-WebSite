﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class MemberRep : DataRepository<Member, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Member> result = new ResultProccess<Member>();
        ResultProccess<UserRole> roleResult = new ResultProccess<UserRole>();

        public override Result<int> Delete(int id)
        {
            Member m = db.Members.SingleOrDefault(t => t.UserID == id);
            db.Members.Remove(m);
            return result.GetResult(db);
        }

        public override Result<Member> GetById(int id)
        {
            return result.GetT(db.Members.SingleOrDefault(m => m.UserID == id));
        }

        public override Result<List<Member>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Member item)
        {
            db.Members.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Member>> List()
        {
            return result.GetListResult(db.Members.ToList());
        }

        public override Result<int> Update(Member item)
        {
            Member m = db.Members.SingleOrDefault(t => t.UserID == item.UserID);
            m.FirstName = item.FirstName;
            m.LastName = item.LastName;
            m.Password = item.Password;
            m.Email = item.Email;
            m.RoleID = item.RoleID;
            m.Photo = item.Photo;
            m.Phone = item.Phone;
            return result.GetResult(db);
        }

        public Result<List<UserRole>> RoleList()
        {
            return roleResult.GetListResult(db.UserRoles.Where(t=>t.RoleID==1||t.RoleID==2).ToList());
        } 
        public Result<Member> LoginMb(string Email,string Password)
        {
            Member mem = db.Members.SingleOrDefault(m => m.Password == Password && m.Email == Email);
            return result.GetT(mem);
        }

        public Result<int> RegisterInsert(Member model)
        {
            bool check = db.Members.Any(m => m.Email == model.Email);
            if (check==false)
            {
                db.Members.Add(model);
            }
            return result.GetResult(db);
        }
              
    }
}
