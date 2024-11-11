using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;
using TicketResell_BusinessObject;
using TicketResell_Service;

namespace ProjectGroup.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int CountTicket { get; set; }
        public int CountUser { get; set; }          
        public int Transaction {  get; set; }
        public decimal TotalRevenue { get; set; }

     
        public string[] LastSixMonths { get; set; }
        public List<Transactions> SortedTransactions { get; set; }
        public int[] RecentTransactions { get; set; }

        



        private readonly ITicketService _ticketService;

        private readonly IUserService _userService;

        private readonly ITransactionService _transactionService;
        public DashboardModel(IHttpContextAccessor httpContextAccessor, ITicketService ticketService, IUserService userService, ITransactionService transactionService)
        {
            _httpContextAccessor = httpContextAccessor;

            _ticketService = ticketService;
            _userService = userService;
            _transactionService = transactionService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            string roleIdString = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
            Guid? roleId = string.IsNullOrEmpty(roleIdString) ? (Guid?)null : Guid.Parse(roleIdString);

            if (!string.IsNullOrEmpty(username) && roleId != null)
            {
                if (roleId == Guid.Parse("57FA54D1-7C67-4405-B277-B8A58049FF3C"))
                {
                    return RedirectToPage("/TicketsPage/Index");
                }
            }
            else
            {
                return RedirectToPage("/Authentication/Login");
            }

            CountTicket = await _ticketService.GetTicketCountAsync();
            CountUser = await _userService.GetUsersCount();
            SortedTransactions = await _transactionService.GetTransactions();
            LastSixMonths = GetLastSixMonths();
            RecentTransactions = GetRecentTransactions(LastSixMonths);
            Transaction = await _transactionService.GetCountTransactions();
            TotalRevenue = CalculateTotalRevenue();
            return Page();
        }

        public string[] GetLastSixMonths()
        {
            var months = new List<string>();
            DateTime currentDate = DateTime.Now;

            for (int i = 0; i < 6; i++)
            {
                var month = currentDate.AddMonths(-i).ToString("MMMM");
                months.Add(month);
            }
            months.Reverse();

            return months.ToArray();
        }


        public int[] GetRecentTransactions(string[] months)
        {
            var transactionCounts = new List<int>();


            foreach (var month in months)
            {

                int monthNumber = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;

                var firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


                var transactionsInMonth = SortedTransactions.Where(t => t.TransactionDate >= firstDayOfMonth && t.TransactionDate <= lastDayOfMonth).ToList();
                transactionCounts.Add(transactionsInMonth.Count);
            }

            return transactionCounts.ToArray();
        }
        public decimal CalculateTotalRevenue()
        {
          
            return SortedTransactions.Sum(t => t.Amount); 
        }

    }
}
