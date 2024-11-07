using Microsoft.EntityFrameworkCore;
using TicketResell_DAO;
using TIcketResell_Repository;
using TicketResell_Service;
using TicketResell_Service.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddDbContext<TicketResellContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketResellDBConnection") ?? throw new InvalidOperationException("Connection string 'DBConnect' not found.")));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
