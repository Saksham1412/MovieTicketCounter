using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketDataAccess;

namespace MovieTicketCounter.Controllers
{
    public class TicketController : ApiController
    {

        //Api to get all tickets on a particular time
        public IEnumerable<MovieTicket> Get(DateTime date)
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    return entities.MovieTickets.Where(e => e.Date == date).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Api to get all tickets
        public IEnumerable<MovieTicket> Get()
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    return entities.MovieTickets.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Api to get user details using ticketId
        public MovieTicket Get(int ticketId)
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    return entities.MovieTickets.FirstOrDefault(e => e.TicketId == ticketId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Api to save ticket of a user on a particular time
        public int Post(string userName, string phoneNumber, DateTime date)
        {
            MovieTicket item = new MovieTicket
            {
                UserName = userName,
                PhoneNumber = phoneNumber,
                isExpired = false,
                Date =date,
                MovieName = "Tarzan"
            };
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    List<MovieTicket> list = entities.MovieTickets.Where(e => e.Date == date).ToList();
                    if (list.Count < 20)
                    {
                        entities.MovieTickets.Add(item);
                        entities.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item.TicketId;
        }

        //Api to update a tickets to a given time
        public string Put(int ticketId, DateTime date)
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    MovieTicket item = entities.MovieTickets.Find(ticketId);
                    if (item == null)
                    {
                        return "No record found";
                    }
                    item.Date = date;
                    entities.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {

                return "Failure";
            }
        }


        //Api to update all tickets to expired if there is a difference of 8 hours or more between the current time and ticket time
        public string Put()
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    List<MovieTicket> item = entities.MovieTickets.Where(x => DbFunctions.DiffHours(x.Date,DateTime.Now) >= 8).ToList();
                    if (item == null)
                    {
                        return "No record found";
                    }
                    item.ForEach(x => x.isExpired = true);
                    entities.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {

                return "Failure";
            }
        }

        //Api to delete a ticket given a ticketId
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                    MovieTicket item = entities.MovieTickets.Find(id);
                    if (item == null)
                    {
                        return NotFound();
                    }
                    entities.MovieTickets.Remove(item);
                    entities.SaveChanges();
                    return Ok(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Api to delete all tickets whick are expired
        public IHttpActionResult Delete()
        {
            try
            {
                using (MovieDataBaseEntities entities = new MovieDataBaseEntities())
                {
                   List<MovieTicket>item = entities.MovieTickets.Where(x => x.isExpired == true).ToList();
                    if (item == null)
                    {
                        return NotFound();
                    }
                    entities.MovieTickets.RemoveRange(item);
                    entities.SaveChanges();
                    return Ok(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
