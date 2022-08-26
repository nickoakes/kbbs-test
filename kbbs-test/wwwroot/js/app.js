import BookList from './BookList.js';

let _bookList;

fetch('app?page=0')
    .then(response => response.json())
    .then(data => {

        _bookList = new BookList(data);

        _bookList.SetPaging();

        _bookList.CreateList();

    });
