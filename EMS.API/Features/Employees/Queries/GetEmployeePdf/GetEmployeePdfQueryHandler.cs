using EMS.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EMS.API.Features.Employees.Queries.GetEmployeePdf
{
    public class GetEmployeePdfQueryHandler : IRequestHandler<GetEmployeePdfQuery, byte[]>
    {
        private readonly AppDbContext _context;

        public GetEmployeePdfQueryHandler(AppDbContext context)
        {
            _context = context;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> Handle(GetEmployeePdfQuery request, CancellationToken cancellationToken)
        {
            // Pulls ALL employees from the database table, completely ignoring UI pagination
            var employees = await _context.Employees
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Clean corporate layout framing
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial).FontColor("#2D3748"));

                    // --- PROFESSIONAL HEADER ---
                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(titleCol =>
                            {
                                titleCol.Item().Text("Employee Management System")
                                    .FontSize(22).Bold().FontColor("#1A202C");
                                titleCol.Item().Text("Internal Staff Directory Report")
                                    .FontSize(11).Medium().FontColor("#4A5568");
                            });

                            row.ConstantItem(120).AlignRight().Column(metaCol =>
                            {
                                metaCol.Item().Text($"Date: {DateTime.Now:MMM dd, yyyy}")
                                    .FontSize(9).FontColor("#718096").AlignRight();
                                metaCol.Item().Text($"Total Records: {employees.Count}")
                                    .FontSize(9).Bold().FontColor("#4A5568").AlignRight();
                            });
                        });

                        // Subtle geometric accent line matching modern web UIs
                        col.Item().PaddingTop(10).Height(2).Background("#4A5568");
                    });

                    // --- MAIN DATA TABLE ---
                    page.Content().PaddingTop(25).Table(table =>
                    {
                        // 3-Column layout configuration
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3); // Full Name
                            columns.RelativeColumn(4); // Email Address
                            columns.RelativeColumn(3); // Phone Number
                        });

                        // Modern Table Header Layout
                        table.Header(header =>
                        {
                            header.Cell().Background("#1A202C").Padding(10).Text("Full Name").Bold().FontColor(Colors.White);
                            header.Cell().Background("#1A202C").Padding(10).Text("Email Address").Bold().FontColor(Colors.White);
                            header.Cell().Background("#1A202C").Padding(10).Text("Phone No").Bold().FontColor(Colors.White);
                        });

                        // Alternating row colors for clean data readability
                        bool isEvenRow = false;

                        foreach (var emp in employees)
                        {
                            var rowBackground = isEvenRow ? "#F7FAFC" : "#FFFFFF";
                            var phoneNumber = string.IsNullOrWhiteSpace(emp.PhoneNumber) ? "—" : emp.PhoneNumber;

                            // Full Name Cell mapped to EmployeeName
                            table.Cell().Background(rowBackground)
                                .BorderBottom(1).BorderColor("#E2E8F0")
                                .Padding(10).Text(emp.EmployeeName ?? "Unnamed Employee");

                            // Email Cell
                            table.Cell().Background(rowBackground)
                                .BorderBottom(1).BorderColor("#E2E8F0")
                                .Padding(10).Text(emp.Email);

                            // Phone Number Cell
                            table.Cell().Background(rowBackground)
                                .BorderBottom(1).BorderColor("#E2E8F0")
                                .Padding(10).Text(phoneNumber);

                            isEvenRow = !isEvenRow;
                        }
                    });

                    // --- PROFESSIONAL FOOTER ---
                    page.Footer().Column(foot =>
                    {
                        foot.Item().PaddingBottom(5).Height(1).Background("#E2E8F0");
                        foot.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Confidential - For Internal Use Only")
                                .FontSize(8).Italic().FontColor("#A0AEC0");

                            row.RelativeItem().AlignRight().Text(x =>
                            {
                                x.Span("Page ").FontSize(9).FontColor("#718096");
                                x.CurrentPageNumber().FontSize(9).Bold().FontColor("#4A5568");
                                x.Span(" of ").FontSize(9).FontColor("#718096");
                                x.TotalPages().FontSize(9).FontColor("#718096");
                            });
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}