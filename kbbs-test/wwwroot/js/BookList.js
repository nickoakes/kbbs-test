export default class BookList {

    //Fields

    books = null;
    selected = null;
    pageCount = 0;
    page = 0;

    //Constructor

    constructor(books) {

        this.books = books;

    }

    //Methods

    SetBindings = () => {

        //Book list

        $('.book-list__element').on('click', e => {

            if ($('.book-information').hasClass('d-none')) {

                $('.card-body').toggleClass('d-none');

            }

            this.SelectBook($(e.delegateTarget).data('reference'));

        });

        //Search type

        $('.search-options .form-check-input').on('change', () => {

            const selected = $('.search-options .form-check-input:checked').val();

            $('#SearchInput').attr('placeholder', selected);

            $('#SearchInput').data('searchtype', selected);

        });

        //Search bar and button

        $('#SearchInput').on('keyup', e => {

            if (e.key == 'Enter') {

                this.BookSearch();

            }

        });

        $('#SearchButton').on('click', () => {

            this.BookSearch();

        });

        //Reset button

        $('#Reset').on('click', () => {

            this.Reset();

        });

    }

    SetPaging = () => {

        fetch('app/BookCount')
            .then(response => response.json())
            .then(data => {

                this.pageCount = data;

                $('.books-pagination .pagination').empty();

                for (let i = 0; i < this.pageCount / 25; i++) {

                    $('.books-pagination .pagination').append(`<li class="page-item"><a class="page-link ${i == 0 ? 'selected' : ''}" data-page="${i}">${i + 1}</a></li>`);

                    $(`.page-link[data-page="${i}"]`).on('click', e => {

                        this.page = i;

                        this.ChangePage();

                        $('.page-link').removeClass('selected');

                        $(e.delegateTarget).addClass('selected');

                    });

                }

            });

    }

    CreateList = () => {

        this.books.forEach(book => {

            const listElement = `<li class="list-group-item book-list__element" data-reference="${book.reference}">${book.title}</li>`;

            $('.list-group').append(listElement);

        });

        this.SetBindings();

    }

    ClearList = () => {

        $('.book-list').empty();

    }

    PopulateCard = () => {

        const selectedBook = this.books.find(x => x. reference == this.selected);

        $('#Title').text(selectedBook.title);

        $('#Author').text(selectedBook.author);

        $('#YearPublished').text(selectedBook.yearPublished);

        $('#Genre').text(selectedBook.genre);

        $('#PageCount').text(selectedBook.pages);

        $('#ISBN').text(selectedBook.isbn);

    }

    SelectBook = reference => {

        this.selected = reference;

        this.PopulateCard();

        $('.book-list__element.selected').removeClass('selected');

        $(`.book-list__element[data-reference="${reference}"]`).addClass('selected');

    }

    ChangePage = () => {

        fetch(`app?page=${this.page}`)
            .then(response => response.json())
            .then(data => {

                this.books = data;

                this.ClearList();

                this.CreateList();

                $(`.book-list__element[data-reference="${this.selected}"]`).addClass('selected');

            });

    }

    BookSearch = () => {

        const searchTerm = $('#SearchInput').val().toLowerCase(),
              searchType = $('#SearchInput').data('searchtype');

        if (searchTerm.trim() != '') {

            fetch(`app/BookSearch?searchTerm=${searchTerm}&searchType=${searchType}`)
                .then(response => response.json())
                .then(data => {

                    this.books = data;

                    this.selected = null;

                    this.page = 0;

                    this.pageCount = 0;

                    this.ClearList();

                    this.CreateList();

                    $('#ResultsCount .alert').text(`${this.books.length} result${this.books.length == 1 ? '' : 's'}`);

                    $('#ResultsCount').removeClass('d-none');

                    $('.books-pagination').addClass('d-none');

                    if ($('.book-select-message').hasClass('d-none')) {

                        $('.card-body').toggleClass('d-none');

                    }

                });

        }

    }

    Reset = () => {

        fetch('app?page=0')
            .then(response => response.json())
            .then(data => {

                this.books = data;

                this.selected = null;

                $('#ResultsCount').addClass('d-none');

                this.ClearList();

                this.CreateList();

                this.page = 0;

                this.ChangePage();

                $('.page-link').removeClass('selected');

                $('.page-link[data-page="0"]').addClass('selected');

                $('.books-pagination').removeClass('d-none');

                $('.book-information').addClass('d-none');

                $('.book-select-message').removeClass('d-none');

                $('#SearchInput').val('');

            });

    }

}
