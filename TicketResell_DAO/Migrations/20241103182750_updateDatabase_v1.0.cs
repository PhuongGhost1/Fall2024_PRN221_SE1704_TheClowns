using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketResell_DAO.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase_v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventType__3213E83FDA2D07AD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role__3213E83FCB30EE41", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    reputation_points = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83FE1FCAFE4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    buyer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    seller_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chat__3213E83FA6801B67", x => x.id);
                    table.ForeignKey(
                        name: "FK__chat__buyer_id__45F365D3",
                        column: x => x.buyer_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__chat__seller_id__45F365D3",
                        column: x => x.seller_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_chat_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    submitted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__feedback__3213E83F84D22FB2", x => x.id);
                    table.ForeignKey(
                        name: "FK__feedback__user_id__4CA06362",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    package_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    subscription_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    valid_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    valid_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__membership__3213E83F19560ECA", x => x.id);
                    table.ForeignKey(
                        name: "FK__membership__user_id__5DCAEF64",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notification__3213E83FEC850A9B", x => x.id);
                    table.ForeignKey(
                        name: "FK__notification__user_id__49C3F6B7",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "social_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__social_media__3213E83F637FDEB7", x => x.id);
                    table.ForeignKey(
                        name: "FK__social_media__user_id__398D8EEE",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "support",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__support__3213E83F58F4AB2F", x => x.id);
                    table.ForeignKey(
                        name: "FK__support__user_id__4F7CD00D",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    owner_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    event_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    event_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    event_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    ticket_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    submitted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    serial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ticket__3213E83F4C9F3B98", x => x.id);
                    table.ForeignKey(
                        name: "FK__ticket__event_ty__6E01572D",
                        column: x => x.event_type_id,
                        principalTable: "EventType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__ticket__owner_id__6D0D32F4",
                        column: x => x.owner_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_role__3213E83FC17315BB", x => x.id);
                    table.ForeignKey(
                        name: "FK__user_role__role_id__5AEE82B9",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__user_role__user_id__59FA5E80",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__image__3213E83FBC9FA28E", x => x.id);
                    table.ForeignKey(
                        name: "FK__image__ticket_id__7D439ABD",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    delivery_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    delivery_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__3213E83F78DE46FD", x => x.id);
                    table.ForeignKey(
                        name: "FK__order__ticket_id__71D1E811",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__order__user_id__70DDC3D8",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    type_transaction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    platform_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    transaction_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    penalty_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    user_type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transact__3213E83F28AA59AE", x => x.id);
                    table.ForeignKey(
                        name: "FK__transaction__ticket__787EE5A0",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_transaction_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chat_buyer_id",
                table: "chat",
                column: "buyer_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_ConversationId",
                table: "chat",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_chat_seller_id",
                table: "chat",
                column: "seller_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_user_id",
                table: "feedback",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_ticket_id",
                table: "image",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_membership_user_id",
                table: "membership",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_user_id",
                table: "notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_ticket_id",
                table: "order",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_user_id",
                table: "order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_social_media_user_id",
                table: "social_media",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_support_user_id",
                table: "support",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_event_type_id",
                table: "ticket",
                column: "event_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_owner_id",
                table: "ticket",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_ticket_id",
                table: "transaction",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_user_id",
                table: "transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "membership");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "social_media");

            migrationBuilder.DropTable(
                name: "support");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
