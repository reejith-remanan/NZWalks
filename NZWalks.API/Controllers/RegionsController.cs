using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Any;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]// https://localhost:1234/api/Regions
    [ApiController]// wii tell the application that the controller is for API use so it automatically validate the model state and gives a 400
                   // response back to the caller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        public RegionsController(NZWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public NZWalksDBContext DBContext { get; }




        // https://localhost:1234/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Hardcoded
            //var regions = new List<Region> 
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl = "https://ewzutrh9t34.exactdn.com/wp-content/uploads/2022/06/Boardwalk-with-kids-on-the-Hobsonville-Point-coastal-walkway-in-Auckland-by-Freewalks.nz-2.jpg?strip=all&lossy=1&ssl=1"
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQZv2BH1j4MAq1gBgyFngtpR1ALjUrOjNnK8Q&s"
            //    }

            //};
            //return Ok (regions);

            //From DB

            var regions = dbContext.Regions.ToList(); // here we sending the domain model to the client(swagger) coupled

            return Ok(regions); // here we are passing the domain model itself to the api viewer swagger not a good practice


            //Get data from DB - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach(var r in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = r.Id,
                    Code = r.Code,
                    Name = r.Name,
                    RegionImageUrl = r.RegionImageUrl,
                });
            }


            //Return DTOs
            return Ok(regionsDto);
        }

        //Get region by Id
        //https://localhost:1234/api/Regions/{id}
        [HttpGet]
        [Route("{id}")]// when we pass an id with the url of the controller it will get mapped from the 'Route' to the input parameter in the method
                       // the name of the parameter in the 'Route' and method should be same.
        public IActionResult GetById(Guid id)
        {
            
            var region = dbContext.Regions.Find(id);//used to fetch data using primary key

            //var regions = dbContext.Regions.FirstOrDefault(x => x.Id == id); this can be used to fetch data using Any property in the table
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);


        }

        //POST to create new region
        //POST:https://localhost:1234/api/Regions
        [HttpPost]

        public IActionResult Create(AddRegionReqDto addRegionReqDto)
        {
            // Map / covert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionReqDto.Code,
                Name = addRegionReqDto.Name,
                RegionImageUrl = addRegionReqDto.RegionImageUrl
            };

            // Use domain model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //new { id = regionDto.Id }:
            //This creates an anonymous object with route values required to call the GetById method.
            //For example, if GetById expects an id parameter, this provides the value(regionDto.Id).
        }


        //Update Region
        //PUT to create new region
        //PUT:https://localhost:1234/api/Regions/{id}
        [HttpPut]

        public IActionResult Update(Guid id, UpdateRegioneDto updateRegioneDto)
        {
            //Ckeck if region exists
            var regionDomainModel =  dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //Map Dto to domain model

            regionDomainModel.Code = updateRegioneDto.Code;
            regionDomainModel.Name = updateRegioneDto.Name;
            regionDomainModel.RegionImageUrl = updateRegioneDto.RegionImageUrl;

            dbContext.SaveChanges();

            //convert domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        //Delete Region
        //Delete:https://localhost:1234/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public IActionResult Delete(Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
