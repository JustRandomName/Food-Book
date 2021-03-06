using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodBook.Domain.Entities.Entities.Recipes;
using FoodBook.Domain.Interfaces.Recipes;
using FoodBook.Infrastructure.DataAccess.Interfaces;
using JetBrains.Annotations;
using MediatR;

namespace FoodBook.Application.Common.Recipes
{
    [UsedImplicitly]
    public class RecipeCreateHandler : IRequestHandler<RecipeCreateRequest, RecipeCreateResponse>
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public RecipeCreateHandler(
            IRecipeService recipeService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _recipeService = recipeService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<RecipeCreateResponse> Handle(RecipeCreateRequest request, CancellationToken cancellationToken)
        {
            Recipe recipe = _mapper.Map<RecipeCreateRequest, Recipe>(request);

            Recipe result = await _recipeService.InsertOrUpdate(recipe);
            await _unitOfWork.Commit<Recipe>();
            return _mapper.Map<Recipe, RecipeCreateResponse>(result);
        }
    }
}