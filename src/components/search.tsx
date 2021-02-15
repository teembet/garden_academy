import { FaSearch } from "react-icons/fa"

export interface SearchProps {
    search: string,
    button_text: string,
    onSearchSubmit: any,
}


const Search: React.SFC<SearchProps> = ({ search, button_text, onSearchSubmit }) => {
    return (
        <>
            <form className="input-group">
                <input type="search" className="search-placeholder form-control" placeholder={" 🔍   " + search} aria-label="Search Front" />

                <div className="input-group-append">
                    <button style={{ width: "125px" }} type="button" onClick={onSearchSubmit} className="btn btn-primary">{button_text}</button>
                </div>
            </form>
        </>
    );
}

export default Search;