// using System.Linq;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.SwaggerGen;

// namespace DatingApp.API
// {
//     public class ReplaceVersionWithExactValueInPath : IDocumentFilter
//     {
//         public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//         {
//             swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary<swaggerDoc.Info.Version>(
//                 path =>
//                 {
//                     return path.Key.Replace("v{version}",swaggerDoc.Info.Version);
//                 }, OpenApiPathItem=>OpenApiPathItem.Value
//             )
//         }
//     }
// }