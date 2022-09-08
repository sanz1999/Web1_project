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
        [HttpPost]
        [Route("api/Comments/AllForOwner")]
        public List<Comment> AllForOwner([FromBody]User person)
        {
            Comments comments = HttpContext.Current.Application["Comments"] as Comments;
            User user = HttpContext.Current.Session["user"] as User;
            List<Comment> selected = new List<Comment>();
            foreach (Comment com in comments.ListOfComments) {
                if (user.OwnedCenters.Contains(com.CenterId)) {
                    selected.Add(com);
                }
            }
            return selected;
        }

        [HttpPost]
        [Route("api/Comments/Approve")]
        public IHttpActionResult Approve( [FromBody] Comment com)
        {
            Comments comments = HttpContext.Current.Application["Comments"] as Comments;
            Comment comm = comments.ListOfComments.Find(o => o.Id == com.Id);
            int index = comments.ListOfComments.IndexOf(comm);
            comm.Approved = true;
            comments.ListOfComments.RemoveAt(index);
            comments.ListOfComments.Insert(index, comm);
            Comments.Save(comments.ListOfComments);
            HttpContext.Current.Application["Comments"] = comments;
            return Ok();


        }
        [HttpPut]
        [Route("api/Comments/NewComment")]
        public IHttpActionResult NewComment([FromBody]Comment newComment)
        {
            Comments comments = HttpContext.Current.Application["Comments"] as Comments;
            newComment.Id = comments.ListOfComments.Count()+1;
            newComment.Approved = false;
            comments.ListOfComments.Add(newComment);
            Comments.Save(comments.ListOfComments);
            HttpContext.Current.Application["Comments"] = comments;
            return Ok();
        }
    }
}
