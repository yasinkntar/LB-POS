
using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Core.Features.Brnach.Queries.Models;
using LB_POS.Data.AppMetaData;
using LB_POS.Web.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LB_POS.Web.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BranchController : POSControllerBase
    {

        [HttpGet(Router.BranchRouteing.GetByID)]
        public async Task<IActionResult> GetBranchByIDAsync([FromRoute] int ID)
        {
            var req = await Mediator.Send(new GetBranchByIDQuery() { ID = ID });
            return NewResult(req);
        }
        [HttpPost(Router.BranchRouteing.AddBranch)]
        public async Task<IActionResult> CreateBranch([FromBody] AddBranchCommand addBranch)
        {
            var req = await Mediator.Send(addBranch);
            return NewResult(req);
        }
        [HttpPut(Router.BranchRouteing.EditeBranch)]
        public async Task<IActionResult> EditeBranch([FromBody] EditBranchCommand addBranch)
        {
            var req = await Mediator.Send(addBranch);
            return NewResult(req);
        }
        [HttpDelete(Router.BranchRouteing.DeleteBranch)]
        public async Task<IActionResult> DeleteBranch([FromRoute] DeleteBranchCommand addBranch)
        {
            var req = await Mediator.Send(addBranch);
            return NewResult(req);
        }
        [HttpGet(Router.BranchRouteing.PaginatedBranch)]
        public async Task<IActionResult> Paginated([FromQuery] GetBrnachPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
