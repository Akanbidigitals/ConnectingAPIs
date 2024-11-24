using ConnectingAPIs.DTO;

namespace ConnectingAPIs.Interface
{
    public interface IProduct
    {
        Task<ResponseDetail> GetProductByID(int id);
        Task<List<Result>> GetAllProducts();
        Task<ResponseDetail> AddProduct(AddProductDTO _adddto);
        Task<ResponseDetail> UpdateProduct(UpdateProductDTO _updatedto);
        Task<ResponseDetail> DeleteProduct(int id);
    }
}
