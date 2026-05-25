using System;
using Catalog.Core;
using MediatR;

namespace Catalog.Application
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
    {
        private readonly ITypeRepository _typeRepository;

        public GetAllTypesHandler(ITypeRepository typeRepository)
        {
            _typeRepository=typeRepository;
        }
        public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typeRepository.GetAllType();
            return typeList.ToResponseList();
        }
    }
}
