using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSource.Models;

namespace WebApiSource.Controllers
{
    public class MainDatasController : ApiController
    {
        private WebApiSourceContext db = new WebApiSourceContext();

        // GET: api/MainDatas
        public IQueryable<MainData> GetMainDatas()
        {
            return db.MainDatas;
        }

        // GET: api/MainDatas/5
        [ResponseType(typeof(MainData))]
        public async Task<IHttpActionResult> GetMainData(int id)
        {
            MainData mainData = await db.MainDatas.FindAsync(id);
            if (mainData == null)
            {
                return NotFound();
            }

            return Ok(mainData);
        }

        // PUT: api/MainDatas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMainData(int id, MainData mainData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainData.Id)
            {
                return BadRequest();
            }

            db.Entry(mainData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MainDatas
        [ResponseType(typeof(MainData))]
        public async Task<IHttpActionResult> PostMainData(MainData mainData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainDatas.Add(mainData);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mainData.Id }, mainData);
        }

        // DELETE: api/MainDatas/5
        [ResponseType(typeof(MainData))]
        public async Task<IHttpActionResult> DeleteMainData(int id)
        {
            MainData mainData = await db.MainDatas.FindAsync(id);
            if (mainData == null)
            {
                return NotFound();
            }

            db.MainDatas.Remove(mainData);
            await db.SaveChangesAsync();

            return Ok(mainData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainDataExists(int id)
        {
            return db.MainDatas.Count(e => e.Id == id) > 0;
        }
    }
}