using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;
using Task = WebApplication1.Models.Task;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly WebApplicationContext _context;
        public ValuesController(WebApplicationContext webApplicationContext)
        {   
            _context = webApplicationContext;
        }
        
        //Template
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Prescription> Get()
        {
            
            
            return _context.Prescription.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentDTO>> GetMedicamentAndPrescriptions(int id)
        {
            //1 medicament
            //2 presc by med
            //1
            var m = await _context.Medicament.Where(x => x.IdMedicament == id).FirstOrDefaultAsync();
            if (m == null)
            {
                return NotFound(id);
            }
            //2
            var prescriptions = await (from p in _context.Prescription
                join pm in _context.Prescription_Medicament on p.IdPrescription equals pm.IdPrescription
                where pm.IdMedicament == id
                    select new Prescription()
                    {
                        IdPrescription = p.IdPrescription,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        IdPatient = p.IdPatient,
                        IdDoctor = p.IdDoctor
                    }
                ).OrderByDescending(x => x.Date)
                .ToListAsync();

            return Ok(new MedicamentDTO
            {
                IdMedicament = m.IdMedicament,
                Name = m.Name,
                Description = m.Description,
                Type = m.Type,
                Prescriptions = prescriptions
            });
        }
        
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //1 find and remove pacient
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound(id);
            }
            
            //2 
            var prescriptions = await _context.Prescription.Where(x => x.IdPatient == id).ToListAsync();

            //List<Prescription_Medicament> pmList = new List<Prescription_Medicament>();
            foreach (Prescription p in prescriptions) 
            {
                //pmList.AddRange(await _context.Prescription_Medicament.Where(x => x.IdPrescription == p.IdPrescription).ToListAsync());
                _context.Prescription_Medicament.RemoveRange(await _context.Prescription_Medicament.Where(x => x.IdPrescription == p.IdPrescription).ToListAsync());
                
            }
            await _context.SaveChangesAsync();
            
            _context.Prescription.RemoveRange(prescriptions);
            await _context.SaveChangesAsync();
            
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
        
        
        /*
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
        
        //Poprawa --------------------------------------------
        
        //Zad 1
        [HttpGet("/api/task/projectId={id}")]
        public async Task<ActionResult<List<TaskDTO>>> GetSelectedOrAllProjects(int id = -1)
        {
            if (id == -1)
            {
                var tasks = await _context.Task.ToListAsync();
                if(tasks.Equals(null))
                {
                    return NotFound();
                }
                

                List<TaskDTO> taskDTOs = new List<TaskDTO>();
                foreach (var t in tasks)
                {
                    var reporter = await _context.User.Where(u => u.IdUser == t.IdReporter).FirstOrDefaultAsync();
                    var assignee = await _context.User.Where(u => u.IdUser == t.IdAssignee).FirstOrDefaultAsync();
                    
                    taskDTOs.Add(new TaskDTO
                    {
                        IdTask = t.IdTask,
                        Name = t.Name,
                        Description = t.Description,
                        CreatedAt = t.CreatedAt,
                        IdProject = t.IdProject,
                        Reporter = reporter,
                        Assignee = assignee
                    });
                }
                
                return Ok(tasks);
            }

            var task = await _context.Task.Where(t => t.IdProject == id).FirstOrDefaultAsync();
            if (task.Equals(null))
            {
                return NotFound();
            }
            
            var reporter2 = await _context.User.Where(u => u.IdUser == task.IdReporter).FirstOrDefaultAsync();
            var assignee2 = await _context.User.Where(u => u.IdUser == task.IdAssignee).FirstOrDefaultAsync();


            return Ok(new List<TaskDTO>{new TaskDTO()
            {
                IdTask = task.IdTask,
                Name = task.Name,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                IdProject = task.IdProject,
                Reporter = reporter2,
                Assignee = assignee2
            }});
        }
        //Zad 2
        
        
    }
    
    
}
