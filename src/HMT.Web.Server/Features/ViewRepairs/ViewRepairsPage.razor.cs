using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Data;
using HMT.Web.Server.Models.Entities;

namespace HMT.Web.Server.Features.ViewRepairs
{
    public partial class ViewRepairsPage : ComponentBase
    {
        public Model Data { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Data = await Mediator.Send(new Query());
        }

        public record Query : IRequest<Model>
        {
        }

        public record Model
        {
            public IEnumerable<RepairOrder> RepairOrders { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly HMTDbContext _db;
            public Handler(HMTDbContext db)
            {
                _db = db;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Model
                {
                    RepairOrders = _db.RepairOrders.ToList()
                };
            }
        }
    }
}
