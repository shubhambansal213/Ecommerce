using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController:ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query=new GetAllProductsQuery(catalogSpecParams);
            var result=await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var query=new GetProductByIdQuery(id);
            var result=await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByProductName(string productName)
        {
            var query=new GetProductByNameQuery(productName);
            var result=await _mediator.Send(query);

            if (result == null || !result.Any())
            {
                return NotFound();
            }
            var dtoList=result.Select(p=>p.ToDto()).ToList();
            return Ok(dtoList);
        }
    }
}