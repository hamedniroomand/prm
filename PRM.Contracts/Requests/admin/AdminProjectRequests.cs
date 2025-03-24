using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PRM.Contracts.Requests.admin
{
    public class AdminProjectRequests
    {
        public class Create
        {
            [MinLength(3)]
            [MaxLength(100)]
            [Required]
            public required string Title { get; init; }

            [AllowNull()]
            public int? ParentId { get; } = null;
        }

        public class Assign
        {
            [Required]
            public int UserId { get; set; }
        }
    }
}