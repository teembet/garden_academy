import { FaSearch } from "react-icons/fa";

export interface SearchProps {
  search: string;
  button_text: string;
  onSearchSubmit: any;
}

const Search: React.SFC<SearchProps> = ({
  search,
  button_text,
  onSearchSubmit,
}) => {
  return (
    <>
      <form className="input-group">
        <input
          type="search"
          className="search-placeholder form-control d-none d-lg-block d-md-block"
          placeholder={" 🔍 " + search}
          aria-label="Search Front"
        />

        <input
          type="search"
          className="search-placeholder form-control d-block d-lg-none d-md-none"
          placeholder={" 🔍 " + search}
          aria-label="Search Front"
          style={{ fontSize: "0.7em", height: "auto" }}
        />

        <div className="input-group-append">
          <button
            style={{ width: "125px" }}
            type="button"
            onClick={onSearchSubmit}
            className="btn btn-primary "
          >
            {button_text}
          </button>
        </div>
      </form>
    </>
  );
};

export default Search;
