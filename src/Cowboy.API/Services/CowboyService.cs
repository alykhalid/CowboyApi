using Cowboy.API.Dtos;
using Cowboy.API.Entities;
using Cowboy.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Cowboy.API.Services
{
    public class CowboyService : ICowboyService
    {
        private readonly ICowboyRepository _cowboyRepository;
        public CowboyService(ICowboyRepository cowboyRepository)
        {
            _cowboyRepository = cowboyRepository;
        }
        public async Task<IEnumerable<CowboyEntity>> ListAsync()
        {
            return await _cowboyRepository.ListAsync();
        }
        public async Task<CowboyResponse> FindByIdAsync(int id)
        {
            var result = await _cowboyRepository.FindByIdAsync(id);
            if (result is null)
                return new CowboyResponse($"Record with Id {id} does not exisit");
            return new CowboyResponse(result);
        }
        public async Task<CowboyResponse> SaveAsync(CowboyDto value)
        {

            HttpClient client = new HttpClient();
            var nameFakeResponse = await client.GetFromJsonAsync<NameFake>("https://api.namefake.com");


            CowboyEntity newEntity = new CowboyEntity();
            newEntity.Id = value.Id;
            newEntity.Name = nameFakeResponse.name;
            newEntity.Hair = nameFakeResponse.hair;
            newEntity.HeightCm = nameFakeResponse.height;
            newEntity.HealthPoints = value.HealthPoints;
            newEntity.HitAccuracy = value.HitAccuracy;
            newEntity.GunName = value.GunName;
            newEntity.ChamberCapacity = value.ChamberCapacity;
            newEntity.CurrentNumOfBullets = value.ChamberCapacity;

            await _cowboyRepository.AddAsync(newEntity);
            _ = _cowboyRepository.Save();

            return new CowboyResponse(newEntity);
        }
        public async Task<bool> UpdateAsync(CowboyEntity value)
        {
            _cowboyRepository.Update(value);
            return _cowboyRepository.Save();
        }
        public async Task<CowboyResponse> DeleteAsync(int id)
        {
            var existingCowboy = await _cowboyRepository.FindByIdAsync(id);
            if (existingCowboy is null)
                return new CowboyResponse($"Record with Id {id} does not exisit");

            _cowboyRepository.Delete(existingCowboy);
            _cowboyRepository.Save();

            CowboyEntity cowboy = null;
            return new CowboyResponse(cowboy);
        }

        public async Task<CowboyResponse> ShootAsync(int id)
        {
            var existingCowboy = await _cowboyRepository.FindByIdAsync(id);
            if (existingCowboy is null)
                return new CowboyResponse($"Record with Id {id} does not exisit");

            if (existingCowboy.CurrentNumOfBullets > 0)
            {
                existingCowboy.CurrentNumOfBullets = existingCowboy.CurrentNumOfBullets - 1;
                _cowboyRepository.Save();
            }
            else
                return new CowboyResponse($"No bullets in the barrel");

            return new CowboyResponse(existingCowboy);
        }

        public async Task<CowboyResponse> ReloadAsync(int id)
        {
            var existingCowboy = await _cowboyRepository.FindByIdAsync(id);
            if (existingCowboy is null)
                return new CowboyResponse($"Record with Id {id} does not exisit");

            existingCowboy.CurrentNumOfBullets = existingCowboy.ChamberCapacity;
            _cowboyRepository.Save();

            return new CowboyResponse(existingCowboy);
        }

        public async Task<object> ShootoutAsync(int FirstCowboyId, int SecondCowboyId)
        {
            dynamic ShootoutSummary = new ExpandoObject();

            var FirstCowboy = await _cowboyRepository.FindByIdAsync(FirstCowboyId);
            if (FirstCowboy is null)
                return new JsonResult($"Record with Id {FirstCowboyId} does not exisit");

            var SecondCowboy = await _cowboyRepository.FindByIdAsync(SecondCowboyId);
            if (SecondCowboy is null)
                return new JsonResult($"Record with Id {SecondCowboyId} does not exisit");

            ShootoutSummary.PlayerOne = new ExpandoObject();
            ShootoutSummary.PlayerOne.Name = FirstCowboy.Name;
            ShootoutSummary.PlayerOne.Gun = FirstCowboy.GunName;
            ShootoutSummary.PlayerOne.Accuracy = FirstCowboy.HitAccuracy;
            ShootoutSummary.PlayerOne.ChamberCapacity = FirstCowboy.ChamberCapacity;

            ShootoutSummary.PlayerTwo = new ExpandoObject();
            ShootoutSummary.PlayerTwo.Name = SecondCowboy.Name;
            ShootoutSummary.PlayerTwo.Gun = SecondCowboy.GunName;
            ShootoutSummary.PlayerTwo.Accuracy = SecondCowboy.HitAccuracy;
            ShootoutSummary.PlayerTwo.ChamberCapacity = SecondCowboy.ChamberCapacity;


            Random rand = new Random();
            bool firstCowboyTurn = rand.Next(1) > 0 ? true : false;
            bool continueLoop = true;
            int Moves = 0;

            ShootoutSummary.Round = new dynamic[5];

            CowboyEntity playerInPlay, playerInWaiting;
            //In place swapping of two variables. https://stackoverflow.com/a/39190792/394381
            (playerInPlay, playerInWaiting) = firstCowboyTurn ? (FirstCowboy, SecondCowboy) : (SecondCowboy, FirstCowboy);

            while (continueLoop)
            {

                ShootoutSummary.Round[Moves] = new ExpandoObject();
                ShootoutSummary.Round[Moves].title = $"Round {Moves + 1}";
                ShootoutSummary.Round[Moves].Turn = playerInPlay.Name;


                if (playerInPlay.CurrentNumOfBullets > 0)
                {
                    playerInPlay.CurrentNumOfBullets--;
                    if (rand.Next(10) <= playerInPlay.HitAccuracy)
                    {
                        ShootoutSummary.Round[Moves].Summary = $"{playerInPlay.Name} fires and hits his opponent";
                        playerInWaiting.HealthPoints--;
                    }
                    else
                    {
                        ShootoutSummary.Round[Moves].Summary = $"{playerInPlay.Name} fires but missed hitting his opponent";
                    }
                }
                else
                {
                    playerInPlay.CurrentNumOfBullets = playerInPlay.ChamberCapacity;
                    ShootoutSummary.Round[Moves].Summary = $"{playerInPlay.Name} has to reload his gun";
                }

                if (playerInWaiting.HealthPoints == 0)
                {
                    _ = await DeleteAsync(playerInWaiting.Id);
                    _ = await UpdateAsync(playerInPlay);
                    ShootoutSummary.Result = $"{playerInPlay.Name} is the winner";
                }
                else if (Moves == 4)
                {
                    _ = await UpdateAsync(playerInPlay);
                    _ = await UpdateAsync(playerInWaiting);
                    ShootoutSummary.Result = "After five gruelling rounds, its a draw";
                }

                Moves++;
                continueLoop = playerInWaiting.HealthPoints > 0 && Moves < 5;

                //In place swapping of two variables. https://stackoverflow.com/a/39190792/394381
                (playerInPlay, playerInWaiting) = (playerInWaiting, playerInPlay);
            }

            return ShootoutSummary;
        }

    }
}
