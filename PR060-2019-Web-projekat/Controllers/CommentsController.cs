using PR060_2019_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR060_2019_Web_projekat.Controllers
{
    public class CommentsController : ApiController
    {
        public List<Comment> GetCommentsForFC(int id) {
            Comments comments = HttpContext.Current.Application["Comments"] as Comments;
            List<Comment> selected = new List<Comment>();
            foreach (Comment com in comments.ListOfComments) {
                if ((com.CenterId == id) && com.Approved) {
                    selected.Add(com);
                }
            }
            return selected;
        }
    }
}
