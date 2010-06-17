ctx.Companies.Where(function(c){
  return c.Employees.Count == 2;
}).Select(function(c){
  return {Name : c.Name};
});