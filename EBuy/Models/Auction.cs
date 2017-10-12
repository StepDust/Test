using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBuy.Models {
    public class Auction {
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// 必填，最大长度：50
        /// </summary>
        [Required, StringLength(50, MinimumLength = 0)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 初始价格
        /// 范围：[1, 1000000]
        /// </summary>
        [Range(1, 1000000)]
        public decimal StarPrice { get; set; }
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// 开始时间
        /// 范围：[2000-01-01,2222-01-01]
        /// </summary>
        [Range(typeof(DateTime), "1/1/2000", "1/1/2222")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// 范围：[2000-01-01,2222-01-01]
        /// </summary>
        [Range(typeof(DateTime), "1/1/2000", "1/1/2222")]
        public DateTime EndTime { get; set; }
    }
}