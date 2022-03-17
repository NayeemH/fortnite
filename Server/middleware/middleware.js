const jwt = require ("jsonwebtoken");

/*It is a funciton whisch can be used in any route in the middlewire [i.g. route.get("/",req,checklogin,res)]
so that before routing in anywhere it will check the jwt token form the user and let him access*/

const cehckLogin = (req,res,next)=>
{
    /*request will come at Header from user, when the user will hit the route here will be a authorization section
    where there will be Bearer adhakjw23n.. (jwt Token)*/

    const {authorization} = req.headers; //it is calling from authorization
    try{
        
        console.log("JWT Authorization");
        const token = authorization.split(' ')[1]; //spliting the word Bearer and taking the token
        const decoded = jwt.verify(token,process.env.JWT_SECRET_TOKEN); //jwt.verify will verify the token
        const {usrname, userId} = decoded;
        req.username = username;
        req.userId = userId; //in the next routes if the username or userid is needed
        next(); //after this it will go to next
       
    }
    catch
    {
        next("Authentication Failed!");
        console.log("Authentication Failed!");

    }

}

module.exports = checkLogin;

/* const checkLogin = require("/MiddleWare");*/
