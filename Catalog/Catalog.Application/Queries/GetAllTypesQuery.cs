using System;
using System.Collections;
using MediatR;

namespace Catalog.Application
{
    public class GetAllTypesQuery:IRequest<IList<TypeResponse>>
    {
        
    }
}
