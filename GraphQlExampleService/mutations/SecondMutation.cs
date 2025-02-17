[ExtendObjectType("Mutation")]
public class SecondMutation 
{
    public async Task<BookPayload> AddBook(BookInput input, [Service] Repository repository)
    {
        var author = await repository.GetAuthor(input.author) ?? 
                        throw new Exception("Author not found");
        var book = new Book(Guid.NewGuid(), input.title, author);
        await repository.AddBook(book);
        return new BookPayload(book);
    }    
}