using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public interface IeStarRepository
    {
        //award
        void Create(Award award);
        void Delete(int id);
        IEnumerable<Award> GetAllAwards();
        int SaveChanges();
    }
}