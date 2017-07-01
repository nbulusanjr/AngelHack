using AutoMapper;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo.DAL
{
   public class UserModelPaged
    {
        public List<UsersModel> Records { get; set; }
        public long TotalRecords { get; set; }
        public long PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentRecordCount { get; set; }

        private IMapper mapper;


        public UserModelPaged()
        {
            var newConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<user, UsersModel>();        
                

            });

            mapper = newConfig.CreateMapper();
        }


        public UserModelPaged GetList(UsersModel user, string search = null, int startIndex = 1, int pageSize = 10, string sortBy = "ID DESC")
        {
            var db = new angelhackEntities();


            var propertyInfo = typeof(user).GetProperty("ID");

            OrderByDirection sortDirec = OrderByDirection.Ascending;

            var resultset = db.users.Where(a =>

             
                                  (
                                 (search == null ? true : a.ID.ToString().Contains(search)) ||
                                 (search == null ? true : a.Fullname.Contains(search)) ||
                                 (search == null ? true : a.Email.Contains(search)) 
                        
                                  )
                             
                                 
                ).OrderBy(x => propertyInfo.GetValue(x, null), sortDirec);





            var totalRowCount = resultset.Count();
            var recordTemp = resultset.Skip((startIndex - 1) * pageSize).Take(pageSize).ToList();


            UserModelPaged pagedRecords = new UserModelPaged();
            pagedRecords.PageNumber = startIndex;
            pagedRecords.TotalRecords = totalRowCount;

            pagedRecords.Records = mapper.Map<List<user>, List<UsersModel>>(recordTemp);

            var totalPages = (int)Math.Ceiling((decimal)pagedRecords.TotalRecords / (decimal)pageSize);
            var currentPage = startIndex == 0 ? (int)startIndex : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (startIndex >= totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            pagedRecords.CurrentPage = startIndex;
            pagedRecords.StartPage = startPage;
            pagedRecords.EndPage = endPage;
            pagedRecords.TotalPages = totalPages;
            pagedRecords.PageSize = pageSize;
            pagedRecords.CurrentRecordCount = pagedRecords.Records.Count();

            return pagedRecords;


        }



    }
}
