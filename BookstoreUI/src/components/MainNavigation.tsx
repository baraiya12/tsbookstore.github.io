import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { RoutePaths } from "../utils/enum";
import BookList from "../pages/book-listing";
import PrivateRoute from "./PrivateRoute";
import Book from "../pages/book";
import Cart from "../pages/cart";
import EditBook from "../pages/book/editBook";

//component lazy loading
const Home = lazy(() => import("../pages/register/index"));
const Login = lazy(() => import("../pages/login/index"));
const Register = lazy(() => import("../pages/register"));

const AppRoutes: React.FC = () => {
  return (
    <Switch>
     <Route exact path={RoutePaths.Login} component={Login} />
			<Route exact path={RoutePaths.Register} component={Register} />
			<Route exact path={RoutePaths.BookListing} component={BookList} />
      <PrivateRoute exact path={RoutePaths.Book} component={Book} />
			<PrivateRoute exact path={RoutePaths.AddBook} component={EditBook} />
			<PrivateRoute exact path={RoutePaths.EditBook} component={EditBook} />
      <PrivateRoute exact path={RoutePaths.Cart} component={Cart} />
    </Switch>
  );
};

export default AppRoutes;
