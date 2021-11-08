﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    public class RequestLog
    {
        [Key]
        public int RequestLogId { get; set; }
        public string TraceIdentifier { get; set; }
        public string ClientIP { get; set; }
        public string URI { get; set; }
        public DateTime RequestDate { get; set; }
        public string Payload { get; set; }
        public string Method { get; set; }
        public string Token { get; set; }
        public int? UserId { get; set; }
        public virtual ResponseLog ResponseLog { get; set; }
        [ForeignKey("ResponseLog")]
        public int ResponseLogId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
