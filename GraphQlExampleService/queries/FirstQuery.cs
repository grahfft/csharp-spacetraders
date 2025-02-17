[ExtendObjectType("Query")]
public class FirstQuery {
    public string Hello() => "World";

    public Task<List<Book>> GetBooks([Service] Repository repository) =>
         repository.GetBooksAsync();
}