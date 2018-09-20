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
    public class BranchDatasController : ApiController
    {
        private WebApiSourceContext db = new WebApiSourceContext();

        // GET: api/BranchDatas
        public IQueryable<BranchData> GetBranchDatas()
        {
            return db.BranchDatas;
        }

        // GET: api/BranchDatas/5
        [ResponseType(typeof(BranchData))]
        public async Task<IHttpActionResult> GetBranchData(int id)
        {
            BranchData branchData = await db.BranchDatas.FindAsync(id);
            if (branchData == null)
            {
                return NotFound();
            }

            return Ok(branchData);
        }

        // PUT: api/BranchDatas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBranchData(int id, BranchData branchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branchData.Id)
            {
                return BadRequest();
            }

            db.Entry(branchData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchDataExists(id))
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

        // POST: api/BranchDatas
        [ResponseType(typeof(BranchData))]
        public async Task<IHttpActionResult> PostBranchData(BranchData branchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BranchDatas.Add(branchData);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = branchData.Id }, branchData);
        }

        // DELETE: api/BranchDatas/5
        [ResponseType(typeof(BranchData))]
        public async Task<IHttpActionResult> DeleteBranchData(int id)
        {
            BranchData branchData = await db.BranchDatas.FindAsync(id);
            if (branchData == null)
            {
                return NotFound();
            }

            db.BranchDatas.Remove(branchData);
            await db.SaveChangesAsync();

            return Ok(branchData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchDataExists(int id)
        {
            return db.BranchDatas.Count(e => e.Id == id) > 0;
        }
    }
}