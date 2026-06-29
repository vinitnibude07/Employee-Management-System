using MediatR;

namespace EMS.API.Features.Employees.Queries.GetEmployeePdf
{
    public record GetEmployeePdfQuery : IRequest<byte[]>;
}