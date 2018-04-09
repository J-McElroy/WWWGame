using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.DataAccessLibrary.Model
{
    [Table(Name = "Images")]
    class Image
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(DbType = "image")]
        public byte[] Data { get; set; }
        [Column]
        public int QuestionId { get; set; }
        [Column]
        public ImageType Type { get; set; }
    }

    internal enum ImageType
    {
        Question,
        Answer,
        Comment
    }
}
