export class Search
{
	constructor(searchInput)
	{
		this.searchButton = searchInput.querySelector(".search-toggle");
		this.searchExpand = searchInput.querySelector(".search-expand");
		this.searchSubmit = this.searchExpand.querySelector(".search-submit");
		this.bindEvents();
	}

	bindEvents()
	{
		this.searchButton.addEventListener("click", () => this.showSearch());
	}

	showSearch()
	{
		this.searchExpand.classList.toggle("shown");
	}
}