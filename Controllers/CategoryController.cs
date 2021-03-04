using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    //nossa app vai funcionar em EndPonint=URL
    //ex:https://localhost:3000
    //PARA ACESSARMOS CLASSES OU METODOS USAMOS ROUTE E MOSTRAMOS QUAL É O CAMINHO PARA ACESSAR

    [Route("v1/categories")]//neste caso falamos q ele vai acessar com /category
    //ex:https:localhost:3000/category
    public class CategoryController : ControllerBase //Classe q ja vem configurado do AspNet,logo nossa classe ja configura
        //la no Startup com mapControllers
    {
        //[Route("metodo")] //para acessar metodos tbm ai ficaria /category/metodo
       //se n colocarmmos ele cai nesse metodo por padrao com /category
       [HttpGet]
       [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]

        public async Task<ActionResult<Category>> GetById([FromServices] DataContext context, int id)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> Post([FromBody]Category model,[FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar a categoria" });
                 
            }
            
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Put([FromBody]Category model,int id, [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });

            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Delete([FromBody] Category model,[FromServices]DataContext context, int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });

            }
        }


    }
}
