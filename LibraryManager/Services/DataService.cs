using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Data;
using LibraryManager.Models;

namespace LibraryManager.Services
{
    public class DataService
    {
        // ==================== BOOKS ====================
        public List<Book> GetAllBooks()
        {
            try
            {
                using var context = new LibraryContext();
                return context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке книг: {ex.Message}", ex);
            }
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            try
            {
                using var context = new LibraryContext();
                return context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.Title.Contains(searchTerm) || b.ISBN.Contains(searchTerm))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при поиске книг: {ex.Message}", ex);
            }
        }

        public List<Book> FilterBooks(int? authorId, int? genreId)
        {
            try
            {
                using var context = new LibraryContext();
                var query = context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .AsQueryable();

                if (authorId.HasValue)
                    query = query.Where(b => b.AuthorId == authorId.Value);

                if (genreId.HasValue)
                    query = query.Where(b => b.GenreId == genreId.Value);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при фильтрации книг: {ex.Message}", ex);
            }
        }

        public Book? GetBookById(int id)
        {
            try
            {
                using var context = new LibraryContext();
                return context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .FirstOrDefault(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке книги: {ex.Message}", ex);
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                using var context = new LibraryContext();
                context.Books.Add(book);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении книги: {ex.Message}", ex);
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                using var context = new LibraryContext();
                context.Books.Update(book);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении книги: {ex.Message}", ex);
            }
        }

        public void DeleteBook(int id)
        {
            try
            {
                using var context = new LibraryContext();
                var book = context.Books.Find(id);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении книги: {ex.Message}", ex);
            }
        }

        // ==================== AUTHORS ====================
        public List<Author> GetAllAuthors()
        {
            try
            {
                using var context = new LibraryContext();
                return context.Authors.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке авторов: {ex.Message}", ex);
            }
        }

        public Author? GetAuthorById(int id)
        {
            try
            {
                using var context = new LibraryContext();
                return context.Authors.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке автора: {ex.Message}", ex);
            }
        }

        public void AddAuthor(Author author)
        {
            try
            {
                using var context = new LibraryContext();
                context.Authors.Add(author);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении автора: {ex.Message}", ex);
            }
        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                using var context = new LibraryContext();
                context.Authors.Update(author);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении автора: {ex.Message}", ex);
            }
        }

        public void DeleteAuthor(int id)
        {
            try
            {
                using var context = new LibraryContext();
                var author = context.Authors.Find(id);
                if (author != null)
                {
                    context.Authors.Remove(author);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении автора: {ex.Message}", ex);
            }
        }

        // ==================== GENRES ====================
        public List<Genre> GetAllGenres()
        {
            try
            {
                using var context = new LibraryContext();
                return context.Genres.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке жанров: {ex.Message}", ex);
            }
        }

        public Genre? GetGenreById(int id)
        {
            try
            {
                using var context = new LibraryContext();
                return context.Genres.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке жанра: {ex.Message}", ex);
            }
        }

        public void AddGenre(Genre genre)
        {
            try
            {
                using var context = new LibraryContext();
                context.Genres.Add(genre);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении жанра: {ex.Message}", ex);
            }
        }

        public void UpdateGenre(Genre genre)
        {
            try
            {
                using var context = new LibraryContext();
                context.Genres.Update(genre);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении жанра: {ex.Message}", ex);
            }
        }

        public void DeleteGenre(int id)
        {
            try
            {
                using var context = new LibraryContext();
                var genre = context.Genres.Find(id);
                if (genre != null)
                {
                    context.Genres.Remove(genre);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении жанра: {ex.Message}", ex);
            }
        }
    }
}
